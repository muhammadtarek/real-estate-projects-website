/* Adapt text area height to the munmber of lines */
function auto_grow(element) {
    element.style.height = "40px";
    element.style.height = (element.scrollHeight) + "px";
}