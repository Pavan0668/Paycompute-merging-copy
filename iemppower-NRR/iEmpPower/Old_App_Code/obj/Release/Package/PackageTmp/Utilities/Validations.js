//Validation to check if a value selected in dropdown list.
function DropDownListSelected(drpDwnCtrl) {

    if (drpDwnCtrl.value == 0) {

        drpDwnCtrl.focus();
        return false
    }

    else {
        return true
    } 
}
// Validation to check if value entered or not in a mandatory text box control fields like
// Produce name, first name, Village name, Agreement number etc.
function TextBoxEmpty(txtBoxCtrl) {
    if (txtBoxCtrl.value.trim() == "") {
        txtBoxCtrl.focus();
        return false
    }
    else {
        return true
    } 
}
// Validation to check minimum length required 
// in a text box controls for first name,
// last name, Produce name etc.
 function TextBoxMinLength(txtBoxCtrl, MinLength) {
     if ((txtBoxCtrl.value.trim().length) < MinLength) {
         txtBoxCtrl.focus();
         return false
     }
     else {
         return true
     } 
 }

// Validation to check maximum 
// characters accepted in a text box controls for first name,
// last name, Produce name etc.
function TextBoxMaxLength(txtBoxCtrl, MaxLength) {
    if ((txtBoxCtrl.value.trim().length) > MaxLength) {
        txtBoxCtrl.focus();
        return false
    }
    else {
        return true
    } 
}
 function TestNumber(txtBoxCtrl) {
     if ((txtBoxCtrl.value.trim()) >= 367 || (txtBoxCtrl.value.trim()) <= 0) {
         txtBoxCtrl.focus();
         return false
     }
     else {
         return true;
     }
 }

function TxtBoxMinValue(txtBoxCtrl) {
    if ((txtBoxCtrl.value.trim()) <= 0 || (txtBoxCtrl.value.trim()) >= 1000) {
        txtBoxCtrl.focus();
        return false
    }
    else {
        return true;
    }
}
function TextBoxValue(txtBoxCtrl) {
    if ((txtBoxCtrl.value.trim()) <= 0 || (txtBoxCtrl.value.trim()) >= 100) {
        txtBoxCtrl.focus();
        return false
    }
    else {
        return true;
    }
}
function TxtBoxRangeValue(txtBoxCtrl) {
    if ((txtBoxCtrl.value.trim()) <= 0 || (txtBoxCtrl.value.trim()) >= 10000) {
        txtBoxCtrl.focus();
        return false
    }
    else {
        return true;
    }
}
function TxtBoxQuantityValue(txtBoxCtrl) {
    if ((txtBoxCtrl.value.trim()) <= 0 || (txtBoxCtrl.value.trim()) >= 100000) {
        txtBoxCtrl.focus();
        return false
    }
    else {
        return true;
    }
}
function TestIdNumber(txtBoxCtrl) {
    if ((txtBoxCtrl.value.trim()) >= 9999 || (txtBoxCtrl.value.trim()) <= 0) {
        txtBoxCtrl.focus();
        return false
    }
    else {
        return true;
    }
}
// Compare the two values
// require a minimum of value and a maximum of value

function TextBoxCompare(txtBoxCtrl, minValue, maxValue) {
    if ((txtBoxCtrl.value.trim()) <= minValue || (txtBoxCtrl.value.trim()) >= maxValue) {
        txtBoxCtrl.focus();
        return false
    }
    else {
        return true;
    }
}

// To che checking if control is 0 value
function TextBoxZeroValue(txtBoxCtrl) {
    if (txtBoxCtrl.value.trim() <= 0) {
        txtBoxCtrl.focus();
        return false;
    }
    else {
        return true;
    }

}

function TextBoxDotCharachter(txtBoxCtrl) {

    if (isNaN(txtBoxCtrl.value)) {
        txtBoxCtrl.focus();
        return false;
    }
    else {
        return true;
    }
}

function getAgeDiff(DateOfBirth, DateOfJoin) {


    var dtBirth = new Date(DateOfBirth); // Date Should In dd-MMM-yyyy
    var milsecDiff = Math.abs(DateOfJoin - dtBirth); // You Will get Diffrence in Milliseconds

    var Year = parseInt(Math.abs(milsecDiff / (1000 * 3600 * 24 * 365)));
    var Month = parseInt(Math.abs((milsecDiff - (Year * (1000 * 3600 * 24 * 365))) / (1000 * 3600 * 24 * 30)));
    var Day = parseInt(Math.abs((milsecDiff - ((Year * (1000 * 3600 * 24 * 365)) + (Month * (1000 * 3600 * 24 * 30)))) / (1000 * 3600 * 24)))
    return Year;
}

//Time validation function TIME Format EX: 10:11AM or 10:11 AM 
//var strTimeReg = new RegExp(/^((1[012]|[1-9]):[0-5][0-9](\\s)?(?i)(am|pm))$/);
var strTimeReg = new RegExp(/^([1-9]|1[0-2]|0[1-9]){1}(:[0-5][0-9][(\s)][aApP][mM]){1}$/);
function validateTime(txtBoxCtrl) {
    var strTime = txtBoxCtrl.value.trim();
    var strTimeMatch = strTime.match(strTimeReg);
    if (strTimeMatch == null) {
        txtBoxCtrl.focus();
        return false;
    }
    else {
        return true;
    }
}

//RK:Date validation
function DateValidator(DateTextBox) {
    var regexDate = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
    if (!regexDate.test(DateTextBox.value)) {
        return false;
    }
    else
        return true;
}
//RK:Time validation funct
function TimeValidator(TimeTextBox) {
    var regexTime = /^([0-1][0-9]|2[0-3]):([0-5][0-9])$/;
    if (!regexTime.test(TimeTextBox.value)) {
        return false;
    }
    else
        return true;
}
//RK:date validation function
//Validating from Date should be less than to Date.
function CompareDates(StartDateTextBox, EndDateTextBox) {
    //Start date should be less than end date.
    var FromDate = StartDateTextBox.value.split('-');
    var EndDate = EndDateTextBox.value.split('-');
    var DateTimeValue = 0;
    var DateValue = 'false';
    var StartYear = parseInt(FromDate[2], 10);
    var EndYear = parseInt(EndDate[2], 10);
    var StartMonth = parseInt(FromDate[1], 10);
    var EndMonth = parseInt(EndDate[1], 10);
    var StartDay = parseInt(FromDate[0], 10);
    var EndDay = parseInt(EndDate[0], 10);

    if (parseInt(StartYear) < parseInt(EndYear)) {
        DateValue = 'true';
    }
    else if (parseInt(StartYear) == parseInt(EndYear)) {

        if (parseInt(StartMonth) < parseInt(EndMonth)) {
            DateValue = 'true';
        }
        else if (parseInt(StartMonth) == parseInt(EndMonth)) {
            if (parseInt(StartDay, 10) <= parseInt(EndDay, 10)) {
                DateValue = 'true';
            }
            else if (parseInt(StartDay) > parseInt(EndDay)) {
                DateValue = 'false';
            }
        }
    }
    if (DateValue == 'true') {
        return true;
    }
    if (DateValue == 'false') {
        return false;
    }
}

//to get the current time in 12 hours format
function getCurrentTime() {
    var dTime = new Date();
    var hours = dTime.getHours();
    var minute = dTime.getMinutes();
    var period = "AM";
    if (hours > 12) {
        period = "PM"
    }
    else {
        period = "AM";
    }
    if (minute < 9) {
        minute = "0" + minute;
    }
    //minute = minute.substring(minute.length - 2, minute.length);
    hours = ((hours > 12) ? hours - 12 : hours)
    hours = "0" + hours;
    hours = hours.substring(hours.length - 2, hours.length);
    return hours + ":" + minute + " " + period
}

//Validation to compare selected in dropdown lists .
function CompareDropDownLists(drpDwnCtrl1, drpDwnCtrl2) {

    if (drpDwnCtrl1.value == drpDwnCtrl2.value) {

        drpDwnCtrl2.focus();
        return false
    }

    else {
        return true
    }
}
