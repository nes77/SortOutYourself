#! /usr/bin/env python2

import argparse
import sys
import struct
import random

argp = argparse.ArgumentParser("generate.py")

argp.add_argument("--num", "-n", type=int, default=1000, help="The number of integers to output to the file." +
                                                          " Default: 1000")
argp.add_argument("--out", "-o", type=str, default="test.bin", help="The name of the output file. Default: test.bin")
argp.add_argument("--seed", "-s", type=int, default=0, help="The seed for the random number generator.")

args = argp.parse_args(sys.argv[1:])
random.seed(args.seed)

with open(args.out, "wb+") as out_file:
    for _ in xrange(0, args.num):
        out_file.write(struct.pack("<i", random.randint(-2147483648, 0x7FFFFFFF)))
