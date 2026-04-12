// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//SA LOGIN PAGE ANIMATION NI BAI 
document.addEventListener('DOMContentLoaded', function () {
    var card = document.querySelector('.login-card');
    if (!card) return;

    // Start hidden
    card.style.opacity = 0;
    card.style.transform = 'translateY(30px)';


    // Animate in
    setTimeout(function () {
        card.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
        card.style.opacity = 1;
        card.style.transform = 'translateY(0)';
    }, 100);
});




//SA INDEX PAGE NI BAI 
document.addEventListener('DOMContentLoaded', function () {
    const cards = document.querySelectorAll('.index-card');
    cards.forEach((card, i) => {
        // Start hidden
        card.style.opacity = 0;
        card.style.transform = 'translateY(30px)';

        setTimeout(() => {
            card.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
            card.style.opacity = 1;
            card.style.transform = 'translateY(0)';
        }, 200 + i * 200); 
    });



  } )//index page animation

document.addEventListener('DOMContentLoaded', function () {
    const card = document.querySelector('.create-container');
    if (!card) return;

    // Start hidden
    card.style.opacity = 0;
    card.style.transform = 'translateY(10px)';
    
    setTimeout(function () {
        card.style.transition = 'opacity 0.9s ease, transform 0.9s ease';
        card.style.opacity = 1;
        card.style.transform = 'translateY(0)';
    }, 100);
}); //create page animation







