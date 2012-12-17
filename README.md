ImBatch
=======

I couldn't find any decent image manipulation converters that fitted my needs, so I wrote up a simple tool that does just that.

Example
=======

Compile and run the resulting ImBatch.exe

    ImBatch.exe <input pattern> commands.xml <output folder>

The commands.xml is a xml file that tells ImBatch what to do with it. This xml will resize the input image.

    <batch>
    	<actions>
    		<resize width="2048" height="1546" />
    		<save/>
    	</actions>
    </batch>

This example will do more, though it may not be that useful :)

    <batch>
    	<actions>
    		<resize width="100" height="100"/>
    		<gamma red="0.5" green="0.5" blue="1"/>
    		<bw/>
    		<invert/>
    		<colorfilter filter="red"/>
    		<rotateflip type="Rotate180FlipNone"/>
    		<changeextension ext="png"/>
    		<save/>
    	</actions>
    </batch>

Tips & tricks
=======

Run a single image first(lie img1.png) to test the options, then run a wildcard input(like *.png) to run it for real.