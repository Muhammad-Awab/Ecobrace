function sliderPerView(e, g) {
    var swiper = new Swiper(".mySwiper", {
        slidesPerView: e,
        grabCursor: true,
        spaceBetween: 20,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false,
        },
        pagination: {
            el: ".swiper-pagination",
            clickable: true,
        },
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
    });
}

if (window.screen.width <= 600) {
    sliderPerView(3);
} else if (window.screen.width <= 1400) {
    sliderPerView(4);
} else {
    sliderPerView(5);
}

// Intersection observer for animating elements
const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
        if (entry.isIntersecting) {
            entry.target.classList.remove("animateElement");
        } else {
            entry.target.classList.add("animateElement");
        }
    });
});

// Observe elements with the class "animateElement"
const hiddenElements = document.querySelectorAll(".animateElement");

hiddenElements.forEach((element) => observer.observe(element));