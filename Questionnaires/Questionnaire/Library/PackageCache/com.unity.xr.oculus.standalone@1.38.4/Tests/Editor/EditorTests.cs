using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System;

namespace UnityEditor.XR.Oculus.Standalone
{
    /// <summary>
    /// This class provides tests for the WindowsMR Metro package while in the Editor.
    /// </summary>
    /// <remarks>
    /// Packages require XmlDoc documentation for ALL Package APIs.
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments
    /// </remarks>
    public class OculusStandaloneTests
    {
        /// <summary>
        /// Checks if plugins from the package have been imported.
        /// </summary>
        [Test]
        public void CheckPluginsImported()
        {
            bool pluginFound = false;

            PluginImporter[] importers = PluginImporter.GetImporters(BuildTarget.StandaloneWindows64);
            foreach (PluginImporter importer in importers)
            {
                if (importer.assetPath.Contains("OVRPlugin"))
                {
                    pluginFound = true;
                    break;
                }
            }

            Assert.IsTrue(pluginFound, "Plugins failed to import.");
        }
    }
}