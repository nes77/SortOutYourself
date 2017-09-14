#! /usr/bin/env python2

import argparse
import sys
import struct
import os

argp = argparse.ArgumentParser("check.py")

argp.add_argument("in_file", type=str, help="The file to sort")
argp.add_argument("out_file", type=str, help="The file to write the sorted output to")

args = argp.parse_args(sys.argv[1:])

last_num = -2147483648

nums = []

with open(args.in_file, "rb") as in_file:
    s = os.stat(args.in_file)
    for _ in xrange(0, s.st_size / 4):
        inp = in_file.read(4)
        new_num = struct.unpack("<i", inp)[0]
        nums.append(new_num)

with open(args.out_file, "wb+") as out_file:
    for i in sorted(nums):
        out_file.write(struct.pack("<i", i))

