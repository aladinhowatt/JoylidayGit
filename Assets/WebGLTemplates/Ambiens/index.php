
<?php
if (isset($_GET['search_query'])) {
    $search_query = $_GET['search_query'];
    // Use the search query to perform a search or any other desired action
}
?>
<!DOCTYPE html>
<html lang="en-us">
   <head>
      <meta charset="utf-8" />
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <title>{{{ PRODUCT_NAME }}}</title>
      <meta name="viewport" content="width=device-width, height=device-height,  initial-scale=1.0, user-scalable=no;user-scalable=0;"/>


      <link rel="stylesheet" href="Template/style.css" />
      
   </head>
   <body class="{{{SPLASH_SCREEN_STYLE}}}">
      <!-- Ambiens VR Multiplatform WebGL Template v 1.3-->
      <!-- Unity Container -->
      <div id="main-container" class="fill-window">

         <div id="unity-container">
            <canvas id="unity-canvas"></canvas>
            <div id="toggle_fullscreen" class="button floatbottomLeft fullscreenON" > <span class="imgbutton"></span> </div>
         </div>

         <!-- This is a simple progress bar -->
         <div id="loader" class="loader">
            <div class="container">
               <div class="center">
                  <img src="Template/logo.png" class="logo" />
               </div>
   
               <div class="center">   
                  <div class="progressbar">
                     <div id="fill" class="fill" style="width: 0%;"></div>
                  </div>
               </div>
            </div>
            
            
            <div class="footer-cont">
               <p>{{{ PRODUCT_NAME }}} v. {{{ PRODUCT_VERSION }}}</p>
               <p>{{{ COMPANY_NAME }}}</p>
            </div>

         </div>
         
      </div>

      <script src="Template/app.js"> </script>

   </body>
</html>
