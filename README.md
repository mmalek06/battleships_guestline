# UI folder description

I had never written any console app with an elaborate UI, and I wasn't sure how to approach the problem of drawing UI elements. 
I saw some C# libraries that were designed just for that, but you said to "keep it simple", so I didn't want to use any dependencies
with complex APIs that neither you, nor I would know. <br />
And so I decided to go with something based on what I know from HTML. Imagine the Layout class as an html wrapper element and what's 
inside the Components folder as block (for example div) elements. In html such a setup would stack the block elements on top of each 
other and that's what's happening here.<br /> 
I guess it could have been done simpler, for example by just putting all the visual code into one component - after all the amount 
of code isn't that big - but by doing it like I did, it made it easier for me to think about the layout and use the interactive view 
parts, like drawing X and O at the correct spots. <br />
Obviously in case of the requirement being a more complex one, like building multiple panels, stacking elements not only vertically but 
also horizontally, I would rather use some ready-made library for drawing the UI.

# A word on testing

I didn't unit test the Components folder mostly because I felt like I've spent a lot of time writing this app already and it's way more
important to check if the other parts work well.<br />
I've left some somments in the RandomPlacementOpponentTests. One time I've been writing a Unity-based game, just for fun, and trying to 
test it also resulted in the creation of a lot of testing code that was so complex, that it could use being unit tested on its own.

# Compiling it and running it

Nothing fancy here, you can compile the application the same way you would compile any other application - by doing it in VS or Rider. 
I myself used Rider also for running it. If you decide to compile and run the app using VS, a cmd window will pop up, but when you
finish the game (by winning) a one last message will be displayed that you won't have the chance to see, because the window will disappear.
