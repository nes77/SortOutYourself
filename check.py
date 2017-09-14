#! /usr/bin/env python2

import argparse
import sys
import struct
import os

argp = argparse.ArgumentParser("check.py")

argp.add_argument("check_file", type=str, help="The file to check")

args = argp.parse_args(sys.argv[1:])

last_num = -2147483648

with open(args.check_file, "rb") as in_file:
    s = os.stat(args.check_file)
    for _ in xrange(0, s.st_size / 4):
        new_num = struct.unpack("<i", in_file.read(4))
        if new_num < last_num:
            print "Nope!"
            exit(-1)
        else:
            last_num = new_num


print "Yep!"


