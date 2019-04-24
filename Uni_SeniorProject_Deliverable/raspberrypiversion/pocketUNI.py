import pygame
import random
import RPi.GPIO as GPIO

# Define some colors
BLACK = (0, 0, 0)
WHITE = (255, 255, 255)
RED = (255, 0, 0)
BLUE = (0, 191, 255)

WIDTH = 320
HEIGHT = 240


LASER_SPEED = 7

GPIO.setmode(GPIO.BCM)
GPIO.setup(4, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(19, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(16, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(26, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(5, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(6, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(14, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(15, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(20, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(18, GPIO.IN, pull_up_down = GPIO.PUD_UP)


#This the simple version of the adventures of uni
#This game is to demonstrate the use of our custom controller and screen
#This game will involve character shooting the enemies and keeping track on kill counts
#Our full version of the game is in another link

#Code template and resource provided by  http://programarcadegames.com
#http://programarcadegames.com/python_examples/f.php?file=sprite_collect_graphic_direction.py
#english version provide by Paul Vincent Craven
#pygame documentation : https://www.pygame.org/docs/ref/transform.html



class Player(pygame.sprite.Sprite):

    def __init__(self, x, y):

        # Call the parent's constructor
        super().__init__()
        # positions
        self.x = 0
        self.y = 0

        # Load the image looking to the right
        self.image = pygame.image.load("player1.png").convert()


        #scaling the character
        self.image = pygame.transform.scale(pygame.image.load("player1.png"), (50, 50))

        self.rect = self.image.get_rect()

        # Make our top-left corner the passed-in location.
        self.rect.x = x
        self.rect.y = y

        # -- Attributes
        # Set speed vector
        self.change_x = 0
        self.change_y = 0


    def spawnPlayer(self):

        player = Player()

        player.x = random.randrange(50, WIDTH - 50)
        player.y = random.randrange(50, HEIGHT - 50)




    def changespeed(self, x, y):
        #speed and direction of the player
        self.change_x += x
        self.change_y += y

        # Select if we want the left or right image based on the direction
        # we are moving.
        if self.change_x > 0:
            self.image
        elif self.change_x < 0:
            self.image


    def update(self):

        self.rect.x += self.change_x
        self.rect.y += self.change_y


# This class represents the ball
# It derives from the "Sprite" class in Pygame
class Enemy(pygame.sprite.Sprite):

    def __init__(self):
        super().__init__()

        self.image = pygame.image.load('trackingenemy.png').convert()
        self.image = pygame.transform.scale(pygame.image.load("trackingenemy.png"), (30, 30))

        self.rect = self.image.get_rect()


# Initialize Pygame
pygame.init()

# Set the height and width of the screen
screen = pygame.display.set_mode([WIDTH, HEIGHT])
pygame.display.set_caption("The Adventures Of Uni")

#adds lasers, enemies, and objects
block_list = pygame.sprite.Group()

#adds character
all_sprites_list = pygame.sprite.Group()

for i in range(10):

    enemy = Enemy()

    # Set a random location for the block
    enemy.rect.x = random.randrange(WIDTH)
    enemy.rect.y = random.randrange(HEIGHT)

    # Add the block to the list of objects
    block_list.add(enemy)
    all_sprites_list.add(enemy)

# Create a RED player block
player = Player(50, 50)
all_sprites_list.add(player)

# Loop until the user clicks the close button.
done = False

# Used to manage how fast the screen updates
clock = pygame.time.Clock()

score = 0

# -------- Main Program Loop -----------
while not done:
    #KeyBoard Control
    for event in pygame.event.get():  # User did something
        if event.type == pygame.QUIT:  # If user clicked close
            done = True  # Flag that we are done so we exit this loop
        # Set the speed based on the key pressed
        elif event.type == pygame.KEYDOWN:
            if event.key == pygame.K_LEFT:
                player.changespeed(-3, 0)
            elif event.key == pygame.K_RIGHT:
                player.changespeed(3, 0)
            elif event.key == pygame.K_UP:
                player.changespeed(0, -3)
            elif event.key == pygame.K_DOWN:
                player.changespeed(0, 3)

        # Reset speed when key goes up
        elif event.type == pygame.KEYUP:
            if event.key == pygame.K_LEFT:
                player.changespeed(3, 0)
            elif event.key == pygame.K_RIGHT:
                player.changespeed(-3, 0)
            elif event.key == pygame.K_UP:
                player.changespeed(0, 3)
            elif event.key == pygame.K_DOWN:
                player.changespeed(0, -3)

        #GPIO PROGRAMMING custom gamepad control logic
        
    if (GPIO.input(19) == 0):
        #Move Right
        player.changespeed(0.2555, 0)
    if (GPIO.input(4) == 0):
        #Move Left
        player.changespeed(-0.2555, 0)
    if (GPIO.input(16) == 0):
        #Move Up
        player.changespeed(0, -0.2555)
    if (GPIO.input(26) == 0):
        #Move Down
        player.changespeed(0, 0.2555)
    if (GPIO.input(5) == 0):
        pygame.display.quit()
    if (GPIO.input(6) == 0):
         pygame.time.delay(3000)
    if (GPIO.input(14) == 0):
        print("A-Shoot")
    if (GPIO.input(15) == 0):
        print("B")
    if (GPIO.input(20) == 0):
        print("X")
    if (GPIO.input(18) == 0):
        print("Y")

        

    # Clear the screen
    screen.fill(BLUE)

    all_sprites_list.update()

    # See if the player block has collided with anything.
    blocks_hit_list = pygame.sprite.spritecollide(player, block_list, True)

    # Check the list of collisions.
    for block in blocks_hit_list:
        score += 1
        print(score)

    # Draw all the spites
    all_sprites_list.draw(screen)

    # Limit to 60 frames per second
    clock.tick(60)

    # Go ahead and update the screen with what we've drawn.
    pygame.display.flip()

pygame.quit()
