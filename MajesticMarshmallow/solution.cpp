#include <algorithm>
#include <array>
#include <cstdio>
#include <iostream>
#include <vector>

/* stuff for mmap */
#include <fcntl.h>
#include <sys/mman.h>
#include <sys/stat.h>
#include <sys/types.h>
#include <unistd.h>

static void radix_sort(std::vector<uint32_t> &numbers) {
    const size_t numbers_size = numbers.size();

    for (uint32_t lsd = 0; lsd < 32; ++lsd) {
        std::vector<uint32_t> low, high;
        low.reserve(numbers_size);
        high.reserve(numbers_size);

        for (size_t i = 0; i < numbers_size; ++i) {
            const uint32_t number = numbers[i];
            const uint32_t mask = 1 << lsd;

            if (number & mask) {
                high.push_back(number);
            } else {
                low.push_back(number);
            }
        }

        numbers.clear();
        numbers.insert(numbers.end(), low.begin(), low.end());
        numbers.insert(numbers.end(), high.begin(), high.end());
    }
}

int main(void) {
    int fd = open("numbers.bin", O_RDONLY);
    if (fd == -1) {
        puts("Could not open numbers.bin");
        return EXIT_FAILURE;
    }

    /* I prefer using integer_amount * sizeof(int32_t) over st.st_size just for
     * intention's sake. */
    struct stat st;
    stat("numbers.bin", &st);
    size_t integer_amount = st.st_size / sizeof(int32_t);

    int32_t *integers = (int32_t*) mmap(NULL, integer_amount * sizeof(int32_t), PROT_READ, MAP_SHARED, fd, 0);
    if (integers == MAP_FAILED) {
        puts("Could not create the read mmap.");
        return EXIT_FAILURE;
    }

    /*
     * We convert the nagative numbers to positive numbers here.
     * This is because first of all not every computer represents negative
     * numbers in the same way, so results could vary from computer to
     * computer.
     * My computer however uses two's complement.
     * Which, for technical reasons, puts negative numbers after positive ones,
     * which is obviously not what we want.
     */
    std::vector<uint32_t> positive_numbers;
    std::vector<uint32_t> negative_numbers;
    for (size_t i = 0; i < integer_amount; ++i) {
        int32_t number = integers[i];
        if (number < 0) {
            negative_numbers.push_back(-number);
        } else {
            positive_numbers.push_back(number);
        }
    }

    close(fd);

    /* I'm not sure if reusing the same variables is wise here. */
    fd = open("sorted_numbers.bin", O_RDWR | O_CREAT | O_TRUNC, S_IRUSR | S_IWUSR);
    if (fd == -1) {
        puts("Could not open sorted_numbers.bin");
        return EXIT_FAILURE;
    }

    /* We need to expand the size of the file so that we can fit our numbers */
    lseek(fd, integer_amount * sizeof(int32_t) - 1, SEEK_SET);
    write(fd, "", 1);
    lseek(fd, 0, SEEK_SET);

    integers = (int32_t*) mmap(NULL, integer_amount * sizeof(int32_t), PROT_WRITE, MAP_SHARED, fd, 0);
    if (integers == MAP_FAILED) {
        puts("Could not create the write mmap.");
        return EXIT_FAILURE;
    }

    /* Since we convert the negative numbers to positive numbers to simplify
     * sorting, we need to convert them to descending order instead of
     * ascending */
    radix_sort(negative_numbers);
    for (auto rit = negative_numbers.rbegin(), rend = negative_numbers.rend(); rit != rend; ++rit) {
        int32_t v = *rit;
        *(integers++) = -v;
    }

    radix_sort(positive_numbers);
    for (uint32_t v : positive_numbers) {
        *(integers++) = v;
    }

    close(fd);
}
