<?php
	// php template for receiving the result data.
	// place this file under the directory where you want to store the result data on your server.
	// for customisation, see also ExportToCSV.cs.
 
	$fileName = $_POST["fileName"];
	$inputData = $_POST["inputData"];
	$file = fopen($fileName, "w") or die("Unable to open file!");
	fwrite($file, $inputData);
	echo "Results written remotely at $fileName.";
?>
