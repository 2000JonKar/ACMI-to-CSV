function myFunction() {
  var drive = DriveApp.getFolderById('<YOUR FOLDER ID>').getFiles();
  var activeSheet = SpreadsheetApp.getActiveSpreadsheet().getActiveSheet();
  activeSheet.getRange('A:A').setValue('');
  while (drive.hasNext()) {
    var file = drive.next();
    var nextEmpty = getFirstEmptyRowByColumnArray();
    activeSheet.getRange('A' + getFirstEmptyRowByColumnArray()).setValue(file.getUrl());
  }
}

function getFirstEmptyRowByColumnArray() {
  var spr = SpreadsheetApp.getActiveSpreadsheet();
  var column = spr.getRange('A:A');
  var values = column.getValues();
  var ct = 0;
  while ( values[ct] && values[ct][0] != "" ) {
    ct++;
  }
  return (ct+1);
}

function getCSV(input) {
  
  var data = UrlFetchApp.fetch(input).getAs("text/csv");
  return data;
}