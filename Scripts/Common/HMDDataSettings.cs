/*========================================================================
Product:    #HMDGazeAnalyzer
Developer:  #Jonas Iacobi
Company:    #KTH | SVRVIVE Studios
Date:       #2018-04-06
========================================================================*/

using System.Collections.Generic;
using UnityEngine;

namespace HMDGazeAnalyzing
{
    /// <summary>
    /// This class contains constant variables for use with the recorder and replayer.
    /// </summary>
    public static class HMDDataSettings
    {
        #region Constants
        /// <summary>
        /// The name of the directory in which the data files are to be stored.
        /// </summary>
        public const string DATA_FILE_DIRECTORY = "HMDGazeAnalyzingData";
        /// <summary>
        /// The name of the data files. Files are then separated by version numbers appended to this name.
        /// </summary>
        public const string DATA_FILE_NAME = "gazeRecording";
        /// <summary>
        /// The file ending, determines the file's type.
        /// </summary>
        public const string DATA_FILE_ENDING = ".txt";
        /// <summary>
        /// The maximum amount of data files allowed to be stored.
        /// </summary>
        public const int MAX_DATA_FILE_COUNT = 100;
        #endregion
    }
}