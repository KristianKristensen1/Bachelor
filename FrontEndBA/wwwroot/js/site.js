
function redirectViewToHomePage() {
  
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

function redirectToLogOut() {
    var url = document.URL;
    window.location.replace(url + "/Logout")
}

function myFuntion() {
    var url = document.URL;
    window.location.replace(url + "/participant")
}

function clearBrowser() {
    var url = window.location.href;
    window.history.go(-window.history.length);
    window.location.href = url;
}
