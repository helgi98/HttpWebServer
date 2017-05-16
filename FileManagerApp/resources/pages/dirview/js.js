function renameFile(){
	var newName = prompt("Enter new file name", "Name");
	if(newName !== null){		
		return newName;
	}
}

var path = "/"

$(document).ready(function () {
    refreshData();
})

function refreshData(element) {
    var temp = path;
    if (element === undefined);
    else temp = temp + element.innerHTML + "/";
    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function () {
        if (this.status === 200) {
            if (element == undefined);
            else path = path + element.innerHTML + "/";

            var list = document.getElementById("dirContainer");
            while (list.firstChild) list.removeChild(list.firstChild);
            /*list = document.getElementById("fileContainer");
            while (list.firstChild) list.removeChild(list.firstChild);*/

            console.log(this.responseText);
            var data = JSON.parse(this.responseText.trim());

            var directories = data["directories"];
            var files = data["files"];

            $("#dirContainer").append("<ul id='directories'><h2>Directories</h2></ul>")
            var i = 0
            for (; i < directories.length; ++i) {
                $("#directories").append("<li><p onclick='refreshData(this)'>" + directories[i] + "</p></li>")
            }

            /*$("#fileContainer").append("<ul id='files'><h2>Files</hd></ul>")
            for (i = 0; i < files.length; ++i) {
                $("#files").append("<ul>" + files[i] + "</ul>")
            }*/
        } if (this.status >= 400) {
            path.replace(element.innerHTML, "");
        }
    };
    xhttp.open("GET", "/viewdirectory?path=" + encodeURIComponent(temp), true);
    xhttp.send();
}
