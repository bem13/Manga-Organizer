function CallAPI(a, b) {
    var xhr, request;
    request = {
        'method': 'gdata',
        'gidlist': [[parseInt(a, 10),b]]
    };
    if(request) {
        xhr = new XMLHttpRequest();
        xhr.open('POST', 'http://g.e-hentai.org/api.php', false);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function() {
            if(xhr.readyState === 4 && xhr.status === 200) {
				document.getElementById("content").innerHTML = xhr.responseText;
            }
        };
        xhr.send(JSON.stringify(request));
    }
}

function GetData() {
	var url = prompt("Enter an EH gallery url:", "");
	var chunks = url.split('/');
	CallAPI(chunks[4], chunks[5]);
}