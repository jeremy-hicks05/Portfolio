////console.log('Hello world');
//const firstName = "Georgia";
//const nameLength = firstName.length;

//const artist = "Augusta Savage";
//console.log({ artist });


class CreatePairFactory<Key> {
    key: Key;
    constructor(key: Key) {
        this.key = key;
    }
    createPair<Value>(value: Value) {
        return { key: this.key, value };
    }
}
// Type: CreatePairFactory<string>
const factory = new CreatePairFactory("role");
// Type: { key: string, value: number }
const numberPair = factory.createPair(10);
// Type: { key: string, value: string }
const stringPair = factory.createPair("Sophie");

console.log(`${numberPair.key} , ${numberPair.value}`);
console.log(`${stringPair.key} , ${stringPair.value}`);
