// Đợi 2 giây trước khi ẩn loader và hiển thị nội dung
window.addEventListener('load', function() {
    var loaderWrapper = document.getElementById('loader-wrapper');
    var content = document.getElementById('content');

    setTimeout(function() {
        loaderWrapper.style.display = 'none';
        content.style.display = 'block';
    }, 100); // Thời gian chờ là 2000ms (tức là 2 giây)
});
