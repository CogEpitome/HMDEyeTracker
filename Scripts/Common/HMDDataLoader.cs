﻿/*========================================================================
Product:    #HMDGazeAnalyzer
Developer:  #Jonas Iacobi
Company:    #KTH | SVRVIVE Studios
Date:       #2018-04-13
========================================================================*/

using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace HMDGazeAnalyzing {
    /// <summary>
    /// This class loads data from a data file into memory, for use by the replayer.
    /// </summary>
    public class HMDDataLoader : MonoBehaviour
    {

        #region Public fields
        /// <summary>
        /// The HMDDataLoader instance.
        /// </summary>
        public static HMDDataLoader instance;
        [Tooltip("The name of the file to load from. Exclude directory and file ending.")]
        public string customFileName;
        [Tooltip("Uncheck to discard data points with invalid point and pupil data. Disable to discard data from time spent with eyes closed or not in VR headset.")]
        public bool loadInvalid = true;
        #endregion

        #region Private fields
        //The list containing all of the data objects.
        private List<HMDGazeData> dataList;
        //Keeps track of how many invalid data objects were loaded.
        private int invalidDataCount;
        //The name of the file to load.
        private string fileName;
        #endregion

        #region Unity methods
        private void Awake()
        {
            //Singleton
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            //Initialization
            dataList = new List<HMDGazeData>();
            invalidDataCount = 0;
            SetFileName();
        }


        #endregion

        #region Public methods
        /// <summary>
        /// Check if a data file with the specified index exists.
        /// </summary>
        /// <returns></returns>
        public bool DataFileExists(int index)
        {
            return DataFileExists(fileName, index);
        }

        /// <summary>
        /// Check if a data file with the specified name and index exists.
        /// </summary>
        /// <returns></returns>
        public bool DataFileExists(string fName, int index)
        {
            if (index == 0)
            {
                return File.Exists(Path.Combine(HMDDataSettings.DATA_FILE_DIRECTORY, fName + HMDDataSettings.DATA_FILE_ENDING));
            }
            else
            {
                return File.Exists(Path.Combine(HMDDataSettings.DATA_FILE_DIRECTORY, fName + "(" + index + ")" + HMDDataSettings.DATA_FILE_ENDING));
            }
        }

        /// <summary>
        /// Load all data objects from all data files into a list.
        /// </summary>
        public void LoadAllData()
        {
            int fileIndex = 0;
            while (File.Exists(Path.Combine(HMDDataSettings.DATA_FILE_DIRECTORY, fileName + HMDDataSettings.DATA_FILE_ENDING)))
            {
                LoadData(fileIndex, true);
                fileIndex++;
            }
        }      

        /// <summary>
        /// Load the data from data file with index.
        /// </summary>
        /// <param name="index"></param>
        public void LoadData(int index)
        {
            LoadData(index, false);
        }

        /// <summary>
        /// Get the list of gaze data objects.
        /// </summary>
        /// <returns></returns>
        public List<HMDGazeData> GetData()
        {
            return dataList;
        }

        /// <summary>
        /// Get the gaze data object at index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public HMDGazeData GetData(int index)
        {
            return dataList[index];
        }
        #endregion

        #region Private methods
        //Sets the name of the file to load data from. Basically checks if there is a custom file name, or if the default file name is to be used.
        private void SetFileName()
        {
            fileName = !string.IsNullOrEmpty(customFileName) ? customFileName : HMDDataSettings.DATA_FILE_NAME;
        }

        //Load the gaze data from recording with index fileIndex. Set append to true if reading from several data files.
        private void LoadData(int fileIndex, bool append)
        {
            SetFileName();

            //Set the file path.
            string path;
            if (fileIndex == 0)
            {
                path = Path.Combine(HMDDataSettings.DATA_FILE_DIRECTORY, fileName + HMDDataSettings.DATA_FILE_ENDING);
            }
            else
            {
                path = Path.Combine(HMDDataSettings.DATA_FILE_DIRECTORY, fileName + "(" + fileIndex + ")" + HMDDataSettings.DATA_FILE_ENDING);
            }

            //Check that the file exists.
            if (!File.Exists(path))
            {
                Debug.Log("GazeDataLoader could not find a file matching the path: " + path + ". Please ensure the GazeRecorder has been running.");
                return;
            }

            string[] jsonStrings = File.ReadAllLines(path);

            //Check that data was loaded.
            if (jsonStrings.Length == 0)
            {
                Debug.Log("The data file found at " + path + " was empty.");
                return;
            }

            //If append is false, clear the data list to repopulate it.
            if (!append)
            {
                dataList.Clear();
                invalidDataCount = 0;
            }

            //Load the data
            HMDGazeData tempData = new HMDGazeData();
            for (int i = 0; i < jsonStrings.Length; i++)
            {
                tempData = (HMDGazeData)JsonUtility.FromJson(jsonStrings[i], typeof(HMDGazeData));
                if (tempData.valid || tempData.pupilsValid || loadInvalid)
                {
                    dataList.Add(tempData);
                }
                else
                {
                    invalidDataCount++;
                }
            }

            Debug.Log("HMDDataLoader loaded " + dataList.Count + " data objects, and discarded " + invalidDataCount + " invalid data objects.");
        }
        #endregion
    }
}