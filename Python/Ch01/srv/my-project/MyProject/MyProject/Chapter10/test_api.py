# tests/test_api.py
import re
from unittest.mock import patch, mock_open, call
import pytest
from api import is_valid, export, write_csv

@pytest.fixture
def min_user():
    """Represents a valid user with minimal data."""
    return {
        'email': 'minimal@example.com',
        'name': 'Primus Minimus',
        'age': 18
        }

@pytest.fixture
def full_user():
    """Represents a valid user with full data. """
    return {
        'email': 'full@example.com',
        'name': 'Maximus Plenus',
        'age': 65,
        'role': 'emperor'
        }