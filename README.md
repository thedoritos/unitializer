# Unitializer

Run app from the first scene :rocket: when you're working on Unity Editor.

## What Is This?

You may have a scene that *MUST* be launched at first in your app.
However, when you press "Play" button on Unity Editor, it will launch the scene you are opening (not the one being listed at the top of "Scenes In Build").

Unitializer hooks Play to open the first scene before the app starts.
It also hooks Stop to re-open the scene you were opening after the app terminates.

Happy game developing :)
