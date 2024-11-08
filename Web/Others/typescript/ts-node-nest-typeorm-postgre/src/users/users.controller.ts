import { Body, Controller, Delete, Get, Param, Patch, Post } from "@nestjs/common";
import { UserDto } from "./dtos/user.dto";
import { UsersService } from "./users.service";

@Controller('users')
export class UsersController {
    constructor(private readonly usersService: UsersService) { }

    @Post()
    create(@Body() userDto: UserDto) {
        return this.usersService.save(userDto);
    }

    @Get()
    findAll() {
        return this.usersService.findAll();
    }

    @Get(':id')
    findOne(@Param('id') id: string) {
        return this.usersService.findOneById(+id);
    }

    @Patch()
    update(@Body() userDto: UserDto) {
        return this.usersService.update(userDto);
    }

    @Delete(':id')
    remove(@Param('id') id: string) {
        return this.usersService.delete(+id);
    }
}