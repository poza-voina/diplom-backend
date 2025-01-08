class DropdownButton {
    constructor(id) {
        this.rootNode = document.getElementById(id);
        this.dropdownToggleContainer = this.rootNode.getElementsByClassName("dropdown-toggle-container")[0];
        this.dropdownMenu = this.rootNode.getElementsByClassName("dropdown-menu")[0];

        this.dropdownToggleContainer.addEventListener("click", this.handleDropDownToggle.bind(this));

        for (let item of this.rootNode.getElementsByClassName("dropdown-item")) {
            item.addEventListener("click", this.handleClickOnItem.bind(this));
        }


    }

    handleDropDownToggle(event) {
        this.dropdownMenu.classList.toggle("hidden");
        console.log("dropdown toggled!");
    }

    handleClickOnItem(event) {
        // Скрываем меню, добавляя класс "hidden"
        this.dropdownMenu.classList.add("hidden");

        // Получаем элемент, на который кликнули
        let clickedNode = event.target;
        let toggleNode = this.dropdownToggleContainer.getElementsByClassName("dropdown-item")[0];

        if (clickedNode !== toggleNode) {
            this.swap(clickedNode, toggleNode);
        }


    }


    swap(node1, node2) {
        console.log("SWAP");
        const parent1 = node1.parentNode
        const parent2 = node2.parentNode
        const nextNode1 = node1.nextSibling
        const nextNode2 = node2.nextSibling

        parent1.insertBefore(node2, nextNode1)
        parent2.insertBefore(node1, nextNode2)
    }
}

let dw = new DropdownButton("sorting-button");
