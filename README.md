Google YouTube takeout subscriptions to Tartube
-----------------------------------------------
Yeah not a great name.

## Build Dependencies
* Newtonsoft.JSON
* CommandLineParser

Build targets .NET 4.7.2 and .NET Core 3.1

## Steps
1. Click on your channel icon on the top right of the YouTube UI
2. Click "Your data in YouTube"
3. Click "More" under "Your YouTube dashboard"
4. "Download YouTube data", this will take you to Google takeout
5. Click "All YouTube data included" and deselect everything except subscriptions
6. Click next, complete the wizard by hitting "Create export"
7. When the export is done, download the zip and extract somewhere
8. Run YtTakeout2Tartube.exe per below examples and generate a Tartube JSON
9. In the Tartube UI, import the JSON by clicking Media -> Import into database -> JSON export file
10. Tartube should tell you how many items were exported, if not, then it means it didn't like something in the JSON, try `--strip-colons=true` per below examples

## Examples
Importing from file in the same path

`YtTakeout2Tartube.exe -i subscriptions.json -o tartube.json`

Absolute paths should work too

`YtTakeout2Tartube.exe -i C:\temp\subscriptions.json -o D:\tartube\tartube.json`

If the uploader name has colons it might fail to import, if this happens enable the workaround "strip_colons"

`YtTakeout2Tartube.exe -i subscriptions.json -o tartube.json --strip-colons=true`

There's an option to customize the starting increment of the DB IDs. I don't think this is necessary as it seems the importer doesn't really respect the IDs but the option is there if you need it.

`YtTakeout2Tartube.exe -i subscriptions.json -o tartube.json --db-start-index=50`