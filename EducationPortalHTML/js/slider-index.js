$(document).ready(function () {
	$(".popular-course-carousel").slick(
		{
			dots: true,
			infinite: true,
			speed: 300,
			slidesToShow: 3,
			adaptiveHeight: true
		}
	)
});