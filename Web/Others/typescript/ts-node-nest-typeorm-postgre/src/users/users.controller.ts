import { Body, Controller, Delete, Get, Param, Patch, Post } from "@nestjs/common";
import { UserDto } from "./dtos/user.dto";
import { UsersService } from "./users.service";

@Controller('users')
export class UsersController {
    constructor(private readonly usersService: UsersService) { }

    @Post()
    create(@Body() userDto: UserDto) {
        return this.usersService.create(userDto);
    }

    @Get()
    findAll() {
        return this.usersService.findAll();
    }

    @Get(':id')
    findOne(@Param('id') id: string) {
        return this.usersService.findOne(+id);
    }

    @Patch(':id')
    update(@Body() userDto: UserDto) {
        return this.usersService.update(userDto);
    }

    @Delete(':id')
    remove(@Param('id') id: string) {
        return this.usersService.remove(+id);
    }
}