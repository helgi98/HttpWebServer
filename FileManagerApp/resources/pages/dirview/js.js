function renameFile(){
	var newName = prompt("Enter new file name", "Name");
	if(newName !== null){		
		return newName;
	}
}

var path = "/"

function downloadFile(element)
{
    var fileName = element.value;
    var filePath = path + fileName;
    document.getElementById("_downloadPath").value = filePath;

    var form = document.getElementById("downloadForm");
    form.submit();
}

function formFileElement(fileName)
{
    return `
        <li>
        <p>` + fileName + `
            <input type="image" src="Delete.png" name="delete" title="Delete"/>
            <input type="image" src="Edit.png" name="delete" title="Rename" onclick="renameFile()" />
            <input type="image" src="Download.png" name="download" title="Download"  onclick='downloadFile(this)'
            value='`+ fileName + `'/>
        </p>
					</li>`;
}

function formDirElement(dirName)
{
    return "<li onclick='refreshData(this)'><p>" + dirName + "</p></li>";
}

function clearElement(element)
{
    while (element.firstChild) element.removeChild(element.firstChild);
}

function updateView(directories, files)
{
    clearElement(document.getElementById("directories"));
    for (var i = 0; i < directories.length; ++i) {
        $("#directories").append(formDirElement(directories[i]));
    }

    clearElement(document.getElementById("files"));
    for (i = 0; i < files.length; ++i) {
        $("#files").append(formFileElement(files[i]))
    }
}

$(document).ready(function () {
    refreshData();
})

function refreshData(element) {
    if (element == undefined);
    else path = path + element.firstChild.innerHTML + "/";
    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function () {
        if (this.status === 200) {
            var data = JSON.parse(this.responseText.trim());

            var directories = data["directories"];
            var files = data["files"];

            updateView(directories, files);
        }
    };
    xhttp.open("GET", "/viewdirectory?path=" + encodeURIComponent(path), true);
    xhttp.send();
}
