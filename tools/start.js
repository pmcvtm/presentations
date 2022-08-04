const execSync = require('child_process').execSync;
const slideDir = process.argv[2] || 'DID-NOT-SPECIFY';

if(slideDir === 'DID-NOT-SPECIFY'){
    process.stdout.write("ERROR: You must specify a presentation to launch");
    process.exit();
}

execSync('cd slides/' + slideDir + ' && reveal-md readme.md -w', {stdio:[0, 1, 2]});
