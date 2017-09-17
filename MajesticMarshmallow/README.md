# MajesticMarshmallow

A simple C++14 solution that utilizes a binary LSD Radix Sort to sort numbers.

# Compiling & Running

First off, requirements are Clang-4.0.0 and Make.  
I've not tested with GCC.

Generate a number file and place it in your current working directory under the
name `numbers.bin`.

The output will be in `sorted_numbers.bin`

Compile and run:
```bash
$ make run
```

... That's it.

# Performance Results
On the test file:
time: 48.5 seconds
mem : 2.8 GiB peak
