# Configuration of the robot input and outputs 

In order to correctly import the Robot Input and Output Names form the $config.dat file the following syntax is required.

Under 
```
;==================================
; Userdefined Externals 
;==================================

;FOLD EXTERNALS
```

The format of an Input Signal should be
```
SIGNAL inputname $IN[inputposition]
```
for example
```
SIGNAL STARTBUTTON $IN[81]
```

The format of an Output Signal should be
```
SIGNAL outputname $IN[outputposition]
```
for example
```
SIGNAL BLOWOFF $OUT[81]
```
End with
```
;ENDFOLD EXTERNALS
```



