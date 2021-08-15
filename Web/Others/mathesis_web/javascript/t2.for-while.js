const friends = ['Nikos', 'Kostas', 'Andreas'];
let info = 'My friends are: ';
for (let i = 0; i < friends.length; i++){
    if (i === friends.length - 1){
        info += `and ${friends[i]}.`
    } else {
        info += `${friends[i]}, `
    }
}
console.log(info)



info = 'My friends are: ';
for (friend of friends){
    if (friends.indexOf(friend) === friends.length-1){
        info += `and ${friend}.`
    } else {
        info += `${friend}, `
    }
}
console.log(info)



info = 'My friends are: ';
for (let key in friends){
    if (key == friends.length - 1){
        info += `and ${friends[key]}.`
    } else {
        info += `${friends[key]}, `
    }
}
console.log(info)



info = 'My friends are: ';
let i = 0;
while (i < friends.length ){
    if (i === friends.length - 1){
        info += `and ${friends[i]}.`
    } else {
        info += `${friends[i]}, `
    }
    i++;
}
console.log(info)


info = 'My friends are: ';
i = 0;
do {
    info += `${friends[i]}, `
    i++;
} while (i < friends.length - 1)

do {
    info += `and ${friends[i]}.`
    i++;
} while (i === friends.length - 1)
console.log(info);