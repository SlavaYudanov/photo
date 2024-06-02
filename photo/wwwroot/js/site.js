// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function displayFileName() {
    const fileInput = document.getElementById('uploadedFiles');

    const fileNameSpan = document.getElementById('fileName');

    const uploadButton = document.getElementById('uploadButton');

    const fileCount = fileInput.files.length;

    let arr = [];
    
    // Создаем новую строку для каждого файла
    if (fileInput.files.length > 0) {
        for (var i = 0; i < fileInput.files.length; i++) {
            var fileSize = fileInput.files[i].size; // Размер файла в байтах
            if (fileSize > 70000000) {
                arr.push(fileInput.files[i].name);
                /* var indexToRemove = fileInput.files.indexOf(fileInput.files[i].name); // Находим индекс элемента
                 if (indexToRemove != -1) { // Проверяем, что элемент найден
                     fileInput.files.splice(indexToRemove, 1); // Удаляем элемент
                 } */
            }
            else {
                fileNameSpan.textContent += fileInput.files[i].name;
            }
        }
        if (arr.length == fileInput.files.length) {
            if (fileInput.files.length > 1) {
                fileNameSpan.textContent = '';
                uploadButton.style.display = 'none';
                alert("Все файлы больше 100мб");
            }
        }
        else {
            uploadButton.style.display = 'block';
        }
        if (arr.length > 0) {
            var result = arr.join(', '); // Выводит элементы массива через запятую с пробелом
            alert(result + " Эти файлы больше 100мб"); // Выведет: элемент1, элемент2, элемент3
        }
    } else {
        fileNameSpan.textContent = '';
        uploadButton.style.display = 'none';
    }
}
