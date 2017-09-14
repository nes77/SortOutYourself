# MajesticMarshmallow

A simple C++14 solution that utilizes a binary LSD Radix Sort to sort numbers.

# Issues

Right now the program seg-faults on large files.  
I need to look into this.  
I suspect that `mmap` does not work on large files, but the internet talks
about an `O_LARGEFILE` flag. Shrug.

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
