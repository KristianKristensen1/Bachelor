﻿
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
