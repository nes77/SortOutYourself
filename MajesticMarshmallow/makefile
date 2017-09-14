CC := clang++
CFLAGS := -std=c++14 -Ofast -march=native

SRCDIR := .
OBJDIR := .

SRC := $(wildcard $(SRCDIR)/*.cpp)
OBJ := $(SRC:$(SRCDIR)/%.cpp=$(OBJDIR)/%.o)
EXEC := solution

.PHONY: all
all: $(EXEC)

.PHONY: clean
clean:
	rm -f $(EXEC)
	rm -f $(OBJ)

.PHONY: run
run: $(EXEC)
	$(realpath $(EXEC))

$(EXEC): $(OBJ)
	$(CC) -o $@ $^ $(CFLAGS)

$(OBJDIR)/%.o: $(SRCDIR)/%.cpp
	$(CC) -c -o $@ $< $(CFLAGS)
