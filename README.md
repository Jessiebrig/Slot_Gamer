# Slot_Gamer

This is one of my app that I developed just for fun. I used this app to Autoplay Slot Games from online casino.

Currently it has 2 main functions to play and I call it (Autoplay) and (Autospin)
This app is just basically clicking the spin and stop it for you.
1. It Clicks the Spin button 
2. Capture and Scan captured images every 16 milliseconds on average and stop when it captured the image I was looking for
3. Load the images in Listview for reference and checking whether it reacts at the right time.
This is how I code the (AutoSpin) funtion which is specific only for 1 game which you may select from IGameModel Interface list which create the list of game you want to play.

(Autoplay) This functions has 3modes.
  1st Mode: Click the spin and stop at random time.
  2nd Mode: Click the spin and stop by keep adding the sleep time you set.
  3rd Mode: Click the spin and stop at random time + keep adding the sleep time you set
  
(AutoSpin) This function capture 1 image every 16 milliseconds on average.
Basically its just capturing an image > Scan the image and check if it contains an ARGB value that I have set > and click stop
This app really works since it clicks at a specific time it captures the winning numbers but after finishing the 1st class of a game I realize something.

Slot Games online is like a physical slot games. You can't win if your not in luck.
In the case of online Slot Games, the system will decide randomly whether you win or loss.
