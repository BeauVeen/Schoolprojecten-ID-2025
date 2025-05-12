document.addEventListener("DOMContentLoaded", async () => {
    const storedTheme = localStorage.getItem('theme');
    if (localStorage.getItem('theme') === 'dark') {
        document.body.classList.add('dark-theme');
        document.getElementById('themeIcon').src = 'images/dark-theme.png'
    }
        
    const files = [
        'content/interface-development.txt', 
        'content/miel-noelanders.txt', 
        'content/nicky-jaspers.txt'
    ];

    for (const filePath of files) {
        try {
            const response = await fetch(filePath);
            const data = await response.text();

            if (!data.trim()) continue

            const lines = data.split('\n');
            const heading = lines.shift();
            const content = lines.join('\n')

            const container = document.createElement('div');
            container.style.border = '1px solid #ccc';
            container.style.borderRadius = '8px';
            container.style.padding = '1em';
            container.style.marginBottom = '1em';
            container.classList.add('tile');

            const headerWrapper = document.createElement('div');
            headerWrapper.style.display = 'flex';
            headerWrapper.style.justifyContent = 'space-between';
            headerWrapper.style.alignItems = 'center';
            headerWrapper.style.cursor = 'pointer';

            const header = document.createElement('h2');
            header.innerText = heading;
            header.style.margin = 0;

            const arrow = document.createElement('span');
            arrow.innerText = '+';
            arrow.style.fontWeight = 'bold';
            arrow.style.fontSize = '1.5em';

            const pre = document.createElement('pre');
            pre.innerText = content;
            pre.style.display = 'none';

            const img = document.createElement('img');
            const baseFileName = filePath.split('/').pop().replace('.txt', '');
            img.src = `images/${baseFileName}.jpg`;
            img.alt = `${baseFileName} Picture`;
            img.style.width = '100%';
            img.style.maxWidth = '400px';
            img.style.display = 'none';
            img.style.marginTop = '1em';
            img.onerror = () => img.style.display = 'none';

            headerWrapper.addEventListener('click', () => {
                const isVisible = pre.style.display === 'block';
                pre.style.display = isVisible ? 'none' : 'block';
                img.style.display = isVisible ? 'none' : 'block';
                arrow.innerText = isVisible ? '+' : '-';
            });

            headerWrapper.appendChild(header);
            headerWrapper.appendChild(arrow);
            container.appendChild(headerWrapper);
            container.appendChild(pre);
            container.appendChild(img);

            document.getElementById('text-content').appendChild(container);
            } catch (error) {
                console.error(`Error loading ${filePath}:`, error)
            }
    }
    document.getElementById('startButton').addEventListener('click', () => {
        startColorTransition();
    });

    const themeToggle = document.getElementById('themeToggle');
    themeToggle.addEventListener('click', () => {
        changeColor();
    }); 
});

let currentMode = localStorage.getItem('theme') || 'light';

function getRandomRGB() {
    const r = Math.floor(Math.random() * 256);
    const g = Math.floor(Math.random() * 256);
    const b = Math.floor(Math.random() * 256);
    return `rgb(${r}, ${g}, ${b})`;
}

function startColorTransition() {
    console.log('RGB transition started');
    let interval;

    document.body.classList.add('rgb-transition');

    /*const originalBackground = window.getComputedStyle(document.body).backgroundColor;*/

    interval = setInterval(() => {
        document.body.style.backgroundColor = getRandomRGB();
    }, 100);

    setTimeout(() => {
        clearInterval(interval);
        console.log('RGB transition stopped');

        if (currentMode === 'dark') {
            document.body.classList.add('dark-theme');
            document.getElementById("themeIcon").src = 'images/dark-theme.png';
        } else {
            document.body.classList.remove('dark-theme');
            document.getElementById("themeIcon").src = 'images/light-theme.png';
        }

        document.body.style.backgroundColor = "";
    }, 10000);
}

function changeColor() {
    const body = document.body;
    const icon = document.getElementById("themeIcon");

    const isDark = body.classList.toggle("dark-theme");
    icon.src = isDark ? "images/dark-theme.png" : "images/light-theme.png";

    localStorage.setItem('theme', isDark ? 'dark' : 'light');
    currentMode = isDark ? 'dark' : 'light';
}