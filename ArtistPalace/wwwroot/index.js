/*function changeTextRU(){
	document.getElementById("info-text").innerText = "Небольшой проект, целью которого являетя составление постоянно обновляющегося списка художников со всего мира! Не важно кто ты, маленький, начинающий артист или проффессионал с большой буквы, любитель пикселей или ценитель цифрового искусства, ищешь вдохновения в других художниках или хочешь найти новых работников для своей компании, которые будут продвигать искусство в массы.";
}

function changeTextEN(){
	document.getElementById("info-text").innerText = "A small project aimed at compiling a constantly updated list of artists from all over the world! It doesn't matter who you are, a small, aspiring artist or a professional with a capital letter, a pixel lover or a connoisseur of digital art, looking for inspiration from other artists or want to find new employees for your company who will promote art to the masses.";
}*/

/*function openSearch() {
	if (document.getElementById("search-container").classList.contains("hidden") === false) {
		document.getElementById("search-container").classList.add("hidden");
		document.getElementById("search-container").classList.remove("show");
	}
	else {
		document.getElementById("search-container").classList.remove("hidden");
		document.getElementById("search-container").classList.add("show");
	}
}

function openAddArtist() {
	if (document.getElementById("add-container").classList.contains("hidden") === false) {
		document.getElementById("add-container").classList.add("hidden");
		document.getElementById("add-container").classList.remove("show");
	}
	else {
		document.getElementById("add-container").classList.remove("hidden");
		document.getElementById("add-container").classList.add("show");
	}
}*/

// help overlay
function onOverlay() {
	document.getElementById("overlay").style.display = "block";
}
function offOverlay() {
	document.getElementById("overlay").style.display = "none";
}

/*/ loader
var preloader = document.getElementById('loader');
function preLoaderHandler() {
    preloader.style.display = 'none';
	}
	setTimeout(function() {
		$('#loader').fadeOut('fast');
}, 1500);

window.onscroll = function() {makeStickyNavbar(), makeStickysearch()};

var navbar = document.getElementById("navbar");
var stickyNav = navbar.offsetTop;

var search = document.getElementById("search");
var stickyFil = search.offsetTop + 30;

function makeStickyNavbar() {
  if (window.pageYOffset >= stickyNav) {
    navbar.classList.add("sticky")
  } else {
    navbar.classList.remove("sticky");
  }
}

function makeStickysearch() {
	if (window.pageYOffset >= stickyFil) {
		search.classList.add("sticky-search")
	} else {
		search.classList.remove("sticky-search");
	}
  }*/