from random import randint

char = lambda start: chr(randint(start, start + 25))
capital = lambda: char(65)
lowercase = lambda: char(97)

word = lambda func, length: ''.join([func() for i in range(length)])
cap_word = lambda length: word(capital, length)
low_word = lambda length: word(lowercase, length)

pick = lambda length : cap_word(length) if randint(0, 100) > 33 else low_word(length) if randint(0, 100) > 50 else white(length)
white = lambda length: ''.join([' ' for i in range(length)])

inp = ''.join([pick(length) for length in [randint(3, 12) for i in range(100)]])

gen_inp = lambda min_word_size, max_word_size, num_words: ''.join([pick(length) for length in [randint(min_word_size, max_word_size) for i in range(num_words)]])

def writefile(name, text):
    f = open(name, 'w')
    for line in text:
        f.write(line + '\n')
    f.close()

writefile('mockup.txt', [gen_inp(3, 12, 20) for i in range(20)])
