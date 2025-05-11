function changeColor() {
    const body = document.body;
    const icon = document.getElementById("themeIcon");

    body.classList.toggle("dark-theme");

    if (body.classList.contains("dark-theme")) {
        icon.src = "images/dark-theme.png";
    } else {
        icon.src = "images/light-theme.png";
    }
}

document.addEventListener("DOMContentLoaded", async function () {
    const files = [
        'content/interface-development.txt', 
        'content/miel-noelanders.txt', 
        'content/nicky-jaspers.txt'
    ];

    for (const filePath of files) {
        try {
            const response = await fetch(filePath);
            const data = await response.text();

            const lines = data.split('\n');
            const heading = lines.shift();
            const content = lines.join('\n')

            const container = document.createElement('div');

            const header = document.createElement('h2');
            header.innerText = heading;

            const pre = document.createElement('pre');
            pre.innerText = content;

            container.appendChild(header);
            container.appendChild(pre);

            document.getElementById('text-content').appendChild(container);
            } catch (error) {
                console.error(`Error loading ${filePath}:`, error)
            }
    }
});