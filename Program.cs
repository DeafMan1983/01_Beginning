/*
    Porting from 01 Beginning in C
    http://xopendisplay.hilltopia.ca/2009/Jan/Xlib-tutorial-part-1----Beginnings.html
*/

namespace X11Tutorials;

// TerraFX's Xlib
using TerraFX.Interop.Xlib;
using static TerraFX.Interop.Xlib.Xlib;

// DeafMan1983's ConvFunctions
using static DeafMan1983.ConvFunctions;

unsafe class X11Program
{
    // int main(int argc, char ** argv) { ... }
    int main(string[] args)
    {
        int screen_num, width, height;
        nuint background, border;
        Window win;
        XEvent ev;
        Display *dpy;

        // Let's beginning...
        dpy = XOpenDisplay(null);
        if (dpy == null)
        {
            Console.Error.WriteLine("unable to connect to display\n");
            return 1;
        }

        screen_num = XDefaultScreen(dpy);
        background = XBlackPixel(dpy, screen_num);
	    border = XWhitePixel(dpy, screen_num);

        width = 40;
        height = 40;

        win = XCreateSimpleWindow(dpy, XDefaultRootWindow(dpy), 0, 0, (uint)width, (uint)height, 2, border, background);
        XSelectInput(dpy, win, ButtonPressMask|StructureNotifyMask);

        XMapWindow(dpy, win);

        while(Convert.ToBoolean(1))
        {
            XNextEvent(dpy, &ev);
            switch(ev.type){
            case ConfigureNotify:
                if (width != ev.xconfigure.width
                        || height != ev.xconfigure.height) {
                    width = ev.xconfigure.width;
                    height = ev.xconfigure.height;
                    // printf("Size changed to: %d by %d\n", width, height);
                    Console.WriteLine("Size changed to: {0} by {1}\n", width, height);
                }
                break;
            case ButtonPress:
                XCloseDisplay(dpy);
                break;
            }
        }

        return 0;
    }

    static int Main(string[] args)
    {
        return new X11Program().main(args);
    }
}