
function ShowLabel() {

    $('#inpass').show();
}
function redirectViewToPatientRegister() {
    var url = document.URL;
    window.location.replace(url + "Participant/ParticipentRegister");
}
function redirectViewToResearcherRegister() {
    var url = document.URL;
    window.location.replace(url + "/RegisterResearcher");
}
function redirectViewToCreateStudy() {
    var url = document.URL;
    window.location.replace(url + "/CreateStudy/Index");
}

function myFuntion() {
    var url = document.URL;
    window.location.replace(url + "/participant")
}

function Logout() {
    window.location.replace("google.com")
}

function AllStudies() {
    $('#AllStudies').show();
    $('#MyStudies').hide();
    $('#CreateButton').hide();
}

function MyStudiesResearcher() {
    $('#AllStudies').hide();
    $('#MyStudies').show();
    $('#CreateButton').show();
}

function RelevantStudies() {
    $('#RelevantStudies').show();
    $('#MyStudies').hide();
}

function MyStudiesParticipant() {
    $('#RelevantStudies').hide();
    $('#MyStudies').show();
}

function ShowSelectedParticipant(ID, email) {
    document.getElementById('partID').value = ID;
    document.getElementById('email').value = email;
}

function ShowSelectedResearcher(ID, email) {
    document.getElementById('resID').value = ID;
    document.getElementById('email').value = email;
}

function AllResearchers() {
    $('#UnverifiedResearchers').hide();
    $('#AllResearchers').show();
    $('#unverify').show();
    $('#verify').hide();
}

function UnverifiedResearchers() {
    $('#UnverifiedResearchers').show();
    $('#AllResearchers').hide();
    $('#unverify').hide();
    $('#verify').show();
}
