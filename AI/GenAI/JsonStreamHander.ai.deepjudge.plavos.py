
import json
from io import StringIO

class StreamingJsonParser:
    def __init__(self):
        self.buffer = StringIO()    # Use of StringIO to handle string more efficiently

    def consume(self, input_string: str):
        self.buffer.write(input_string)

    def get(self):
        buffer_value = self.buffer.getvalue()

        attempts = [
            "",         # Original buffer
            "}",        # Attempt closing with a curly brace
            "\"}"       # Attempt closing with a curly brace and quote
        ]

        for attempt in attempts:
            try:
                return json.loads(buffer_value + attempt)
            except json.JSONDecodeError:    #Only catches json.JSONDecodeError to avoid masking other potential issues
                continue

        # Attempt closing after removing last comma
        try:
            last_comma_index = buffer_value.rfind(',')
            return json.loads(buffer_value[:last_comma_index] + "}")
        except json.JSONDecodeError:
            pass

        return {}


# Tests start here

def test_unfinished_StreamingJsonParser(given, expected):
    parser = StreamingJsonParser()
    parser.consume(given)
    print(parser.get())
    assert parser.get() == expected

def test_streaming_json_parser():
    parser = StreamingJsonParser()
    parser.consume('{"foo": "bar"}')
    assert parser.get() == {"foo": "bar"}

def test_chunked_streaming_json_parser():
    parser = StreamingJsonParser()
    parser.consume('{"foo":')
    parser.consume('"bar')
    assert parser.get() == {"foo": "bar"}

def test_partial_streaming_json_parser():
    parser = StreamingJsonParser()
    parser.consume('{"foo": "bar')
    assert parser.get() == {"foo": "bar"}


if __name__ == '__main__':
    test_unfinished_StreamingJsonParser('{"hello":"how", "are":"you", "my":"friend"}', {"hello":"how", "are":"you", "my":"friend"})
    test_unfinished_StreamingJsonParser('{"hello":"how", "are":"you", "my":"friend"', {"hello":"how", "are":"you", "my":"friend"})
    test_unfinished_StreamingJsonParser('{"hello":"how", "are":"you", "my":"friend', {"hello":"how", "are":"you", "my":"friend"})
    test_unfinished_StreamingJsonParser('{"hello":"how", "are":"you", "m', {"hello":"how", "are":"you"})
    test_unfinished_StreamingJsonParser('{"hello":"how"', {"hello":"how"})
    test_unfinished_StreamingJsonParser('{"hello":"how', {"hello":"how"})
    test_unfinished_StreamingJsonParser('{"hello":"', {"hello":""})
    test_unfinished_StreamingJsonParser('{"hello":', {})
    test_unfinished_StreamingJsonParser('{"hell":', {})
    test_streaming_json_parser()
    test_chunked_streaming_json_parser()
    test_partial_streaming_json_parser()

