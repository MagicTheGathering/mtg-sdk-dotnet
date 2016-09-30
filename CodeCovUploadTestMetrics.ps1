# Generate the code coverage file with OpenCover
.\RunUnitTestMetrics.bat

# Download the Codecov script used to upload the code coverage file.
wget https://codecov.io/bash -OutFile CodecovUploader.sh

# Upload the code coverage file to Codecov.io
sh .\CodecovUploader.sh -f "MtgApiManager.Lib_coverage.xml" -t d72b0255da284d4a9ecfd4f077c615f3 -X gcov