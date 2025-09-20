// SPDX-License-Identifier: MIT
pragma solidity ^0.8.24;

contract Store {
    // лишаєм івент?
    event ItemBought(uint256 id, address buyer, uint256 price);

    mapping (address=>uint256) balances;

    address public owner;

    constructor() {
        owner = msg.sender;
    }

    // modifiers
    // modifiers
    // modifiers

    // перевірка чи значення повідомлення не є 0
    modifier valueNotZero() {
        require(msg.value > 0, "Send some ETH.");
        _;
    }

    // перевіряєм чи юзер запрошує стільки коштів скільки ж у нього на балансі
    modifier fairPayment(uint256 _amount) {
        require(balances[msg.sender] >= _amount, "Insufficient funds.");
        _;
    }

    // тільки овнер
    modifier onlyOwner() {
        require(msg.sender == owner, "Only owner can use this.");
        _;
    }

    // functions
    // functions
    // functions

    // вивід для овнера
    function withDrawOwner(uint256 _amount) external payable onlyOwner {
        require(_amount <= address(this).balance, "Insufficient funds.");
        (bool ok, ) = payable(msg.sender).call{value: _amount}("");
        require(ok, "Transaction error.");
    }

    // вивід для простолюду
    function withDraw(uint256 _amount) external payable fairPayment(_amount) {
        (bool ok, ) = payable(msg.sender).call{value: _amount}("");
        require(ok, "Transaction error.");
        balances[msg.sender] -= _amount;
    }

    // депчик
    function deposit() external payable valueNotZero {
        balances[msg.sender] += msg.value;
    }

    // купівля предметів
    function buyItem(uint256 id, address seller, uint256 price) public payable valueNotZero fairPayment(price) {
        require(seller != msg.sender, "You own this item.");
        // знімаєм гроші у покупця
        balances[msg.sender] -= price;
        // віддаєм гроші продавцю з комісією 5%
        balances[seller] += price / 100 * 95;
        emit ItemBought(id, msg.sender, msg.value);
    }

    // get-functions
    // get-functions
    // get-functions

    // юзер у себе на сторінці буде мати власний баланс
    function getBalance() external view returns (uint256) {
        return balances[msg.sender];
    }

    // овнер може переглянути баланс контракту для виведу грошей собі
    function getContractBalance() external view onlyOwner returns (uint256) {
        return address(this).balance;
    }
}