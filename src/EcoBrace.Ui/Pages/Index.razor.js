export function showModal(elementId, plan) {
    if (elementId != null && elementId != undefined) {
        let modalElement = document.getElementById(elementId);
        modalElement.style.display = "block";
        return plan;
    }
    else {
        return "error";
    }
}

export function HideModal(elementId) {
    if (elementId != null && elementId != undefined) {
        let modalElement = document.getElementById(elementId);
        modalElement.style.display = "none";
        return "ok";
    }
    else {
        return "error";
    }
}