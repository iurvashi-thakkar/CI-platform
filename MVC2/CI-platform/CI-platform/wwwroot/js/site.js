let gridbtn = document.getElementById('grid');
let listbtn = document.getElementById('list');
let gbtn = document.getElementById('partialGrid');
let lbtn = document.getElementById('partialList');

gridbtn.addEventListener('click', grid);
listbtn.addEventListener('click', list);

function grid() {
    lbtn.classList.add('d-none');
    gbtn.classList.remove('d-none');

    gridbtn.classList.add("selected-btn");
    listbtn.classList.remove("selected-btn");
    console.log(gridbtn);
}


function list() {
    gbtn.classList.add('d-none');
    lbtn.classList.remove('d-none');
    listbtn.classList.add("selected-btn");
    gridbtn.classList.remove("selected-btn");
}

//let mediawidth = window.matchMedia(max-width:1023px);
//mediawidth.addListener(mediaCheck);

//function mediaCheck() {
//    if (mediawidth.matches) {
//        console.log("entered in criteriya");
//        gbtn.classList.add('d-none');
//        lbtn.classList.remove('d-none');
//    }
//}


function mediaCheck(x) {
    if (x.matches) {
        grid();
    } else {
        list();
    }
}

// Create a MediaQueryList object
let mediawidth = window.matchMedia("(max-width: 1023px)")

// Call the match function at run time:
mediaCheck(mediawidth);

// Add the match function as a listener for state changes:
mediawidth.addListener(mediaCheck);