// Create the tooltips only on document load
$(document).ready(function () {
    $('#content a[rel]').each(function () {
        $(this).qtip(
        {
            content: {
                // Set the text to an image HTML string with the correct src URL to the loading image you want to use
                //text: '<img class="throbber" src="/projects/qtip/images/throbber.gif" alt="Loading..." />',
                url: $(this).attr('rel'), // Use the rel attribute of each element for the url to load
                title: {
                    text: $(this).text(), // Give the tooltip a title using each elements text
                    button: 'Close' // Show a close link in the title
                }
            },
            position: {
                //corner: {
                //    target: 'topRight', // Position the tooltip above the link
                //    tooltip: 'bottomLeft'
                //},
                adjust: {
                    screen: true // Keep the tooltip on-screen at all times
                },
                target: $(document.body), // Position it via the document body...
                corner: 'topCenter' // ...at the center of the viewport
            },
            show: {
                when: 'click',
                solo: true // Only show one tooltip at a time
            },
            //hide: 'unfocus',
            //style: {
            //    tip: true, // Apply a speech bubble tip to the tooltip at the designated tooltip corner
            //    border: {
            //        width: 0,
            //        radius: 4,
            //        color: '#FF4500'
            //    },
            //    name: 'green', // Use the default light style
            //    width: 570 // Set the tooltip width
            //},
            hide: 'unfocus',
            style: {
                width: { max: 800 },
                padding: '14px',
                border: {
                    width: 9,
                    radius: 9,
                    color: '#666666'
                },
                name: 'green'
            },
            api: {
                beforeShow: function () {
                    // Fade in the modal "blanket" using the defined show speed
                    $('#qtip-blanket').fadeIn(this.options.show.effect.length);
                },
                beforeHide: function () {
                    // Fade out the modal "blanket" using the defined hide speed
                    $('#qtip-blanket').fadeOut(this.options.hide.effect.length);
                }
            }
        })
    });

    $('<div id="qtip-blanket">')
      .css({
          position: 'absolute',
          top: $(document).scrollTop(), // Use document scrollTop so it's on-screen even if the window is scrolled
          left: 0,
          height: $(document).height(), // Span the full document height...
          width: '100%', // ...and full width

          opacity: 0.7, // Make it slightly transparent
          backgroundColor: 'black',
          zIndex: 5000  // Make sure the zIndex is below 6000 to keep it below tooltips!
      })
      .appendTo(document.body) // Append to the document body
      .hide(); // Hide it initially
});

function alert_confirm() {
    alertify.confirm("This is a confirm dialog", function (e) {
        if (e) {
            return true;
        } else {
            return false;
        }
    });
}