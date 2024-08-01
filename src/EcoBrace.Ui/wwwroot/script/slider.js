
    let carouselItems = document.querySelectorAll('.carousel .carousel-item');

    carouselItems.forEach((el) => {
        const minPerSlide = 4;
    let next = el.nextElementSibling;
    for (let i = 1; i < minPerSlide; i++) {
            if (!next) {
        // wrap carousel by using first child
        next = carouselItems[0];
            }
    let cloneChild = next.cloneNode(true);
    el.appendChild(cloneChild.children[0]);
    next = next.nextElementSibling;
        }
    });
