window.saveAsFile = function (fileName, byteBase64) {
    var link = this.document.createElement('a');
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + byteBase64;
    this.document.body.appendChild(link);
    link.click();
    this.document.body.removeChild(link);
}

window.loadCssFile = function (filename) {
    try {
        console.log("Loading CSS file:", filename);
        var link = document.createElement("link");
        link.rel = "stylesheet";
        link.href = filename;
        document.head.appendChild(link);
    } catch (error) {
        console.error("Failed to load CSS file:", error);
    }
};

window.DoShowEvent = function (element, Status) {
   
    if ($(element).hasClass('collapse'))
        $(element).collapse(Status);

    if ($(element).hasClass('modal'))
        $(element).modal(Status);

}

//document.addEventListener('DOMContentLoaded', () => {
//    const background = document.querySelector('.background');
//    function createLine() {
//        const line = document.createElement('div');
//        line.classList.add('line');
//        line.style.left = `${Math.random() * 100}vw`;
//        line.style.animationDuration = `${15}s`; // Slower animation
//        line.style.opacity = `${0.3}`;

//        // Randomly decide if the line comes from the top or bottom
//        const fromTop = Math.random() > 0.5;
//        line.style.top = fromTop ? '-100%' : '100%';
//        line.style.animationName = fromTop ? 'fallTop' : 'fallBottom';
        
//        background.appendChild(line);

//        setTimeout(() => {
//            line.remove();
//        }, (parseFloat(line.style.animationDuration) * 10000));
//    }

//    function createStar() {
//        const star = document.createElement('div');
//        star.classList.add('star-t');
//        star.style.left = `${Math.random() * 100}vw`;
//        star.style.top = `${Math.random() * 100}vh`;
//        star.style.animationDuration = `${Math.random() * 3 + 2}s`; // Slower animation
//        star.style.opacity = `${Math.random() * 0.5 + 0.5}`;
//        background.appendChild(star);

//        setTimeout(() => {
//            star.remove();
//        }, (parseFloat(star.style.animationDuration) * 600));
//    }

//    setInterval(createLine, 9000); // Reduced density of lines
//    setInterval(createStar, 100);
//});



