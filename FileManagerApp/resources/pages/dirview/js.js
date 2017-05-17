function renameFile(element) {
    var name = prompt("Enter new file name", "Name");
    if (name != null)
    {
        var oldFileName = element.value;
        var extension = getFileExtension(oldFileName);
        var newFileName = name + "." + (extension == undefined ? "" : extension);

        $.post('/rename',
            { renamePath: path, oldFileName: oldFileName, newFileName: newFileName });

        element.parentElement.firstChild.innerHTML = newFileName;
    }
}

function getFileExtension(fileName)
{
    var re = /(?:\.([^.]+))?$/;

    return re.exec(fileName)[1];  
}

var path = "/";

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
        <p><span>` + fileName + `</span>
            <input type="image" src="Delete.png" name="delete" title="Delete" onclick='deleteFile(this)'
            value='`+ fileName + `'/>
            <input type="image" src="Edit.png" name="rename" title="Rename" onclick='renameFile(this)' 
            value='`+ fileName + `'/>
            <input type="image" src="Download.png" name="download" title="Download"  onclick='downloadFile(this)' 
            value='`+ fileName + `'/>
        </p>
		</li>`;
}

function deleteFile(element)
{
    var filePath = path + element.value;
    $.ajax({
        url: "/delete?path=" + encodeURI(filePath),
        type: 'DELETE'
    });

    var el = element.parentElement.parentElement;
    el.parentElement.removeChild(el);
}

function formDirElement(dirName)
{
    return `<li onclick='refreshData(this)' value='` + dirName + `'><p>` + dirName + `
        </p></li>`;
}

function returnBack()
{
    if (path != "/")
    {
        path = path.substring(0, path.lastIndexOf("/"));
        path = path.substring(0, path.lastIndexOf("/") + 1);
    }
    showDirContent();
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

function refreshData(element)
{
    if (element == undefined);
    else path = path + element.getAttribute('value') + "/";

    showDirContent();
}

function showDirContent() {
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
