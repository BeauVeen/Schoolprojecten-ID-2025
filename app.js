var r = document.querySelector(':root');
var isdark = 0;

function changeColor() {
    if (changeColor == 0) {
        r.style.setProperty('--backgroundcolor','#1a1a1a');
        r.style.setProperty('--bodycolor','#3b3b3b');
        r.style.setProperty('--fontcolor','white');
    } else if (isdark == 1) {
        r.style.setProperty('--backgroundcolor','#d4d4d4')
        r.style.setProperty('--bodycolor','#f2f2f2')
        r.style.setProperty('--fontcolor','black')
        isdark = 0;
    }
}