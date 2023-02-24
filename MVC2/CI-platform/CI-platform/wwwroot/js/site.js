let gridbtn = document.getElementById('grid');
let listbtn = document.getElementById('list');
let gbtn = document.getElementById('partialGrid');
let lbtn = document.getElementById('partialList');

listbtn.addEventListener('click', list);
gridbtn.addEventListener('click', grid);
function list() {
    gbtn.classList.add('d-none');
    lbtn.classList.remove('d-none');
    listbtn.classList.add("selected-btn");
    gridbtn.classList.remove("selected-btn");
}
function grid() {
    
    gbtn.classList.remove('d-none');
    lbtn.classList.add('d-none');
    gridbtn.classList.add("selected-btn");
    listbtn.classList.remove("selected-btn");
   
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
    }
    //else {
    //    list();
    //}
}

// Create a MediaQueryList object
let mediawidth = window.matchMedia("(max-width: 1023px)")

// Call the match function at run time:
mediaCheck(mediawidth);

// Add the match function as a listener for state changes:
mediawidth.addListener(mediaCheck);


//function grid_list_btn_hide(){

//}
//function grid_list_btn_Check(x) {
//    if (x.matches) {
//        gridbtn.classList.add("d-none");
//        listbtn.classList.add("d-none");
//    } else {
//        gridbtn.classList.add("d-block");
//        listbtn.classList.add("d-block");
//    }
//}

//let grid_list_btn = window.matchMedia("(max-width: 700px)")
//grid_list_btn_Check(grid_list_btn);
//grid_list_btn.addEventListener(grid_list_btn_Check)