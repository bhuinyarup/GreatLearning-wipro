document.getElementById("check-btn")?.addEventListener("click", () => {
    const target = document.getElementById("result");
    if (target) {
        target.textContent = "JavaScript from wwwroot is running correctly.";
    }
});
