import Image from "next/image"
import { ExternalLink } from "lucide-react"

export default function Home() {
  return (
    <div className="mx-auto flex flex-col items-center lg:flex-row lg:items-start lg:justify-center lg:max-w-[1260px]">
      {/* Left column - flexible on large screens, 380px on small screens */}
      <div className="w-[380px] lg:flex-1 p-8">
        {/* Profile section */}
        <div className="flex flex-col md:flex-row gap-8 mb-12">
          <div className="flex flex-col items-center md:items-start">
            <div className="relative w-32 h-32 md:w-48 md:h-48 rounded-full overflow-hidden mb-4">
              <Image src="/profile-image.jpg" alt="Profile" fill className="object-cover" priority />
            </div>
            <h1 className="text-3xl md:text-4xl font-bold mb-4">Daniil Filatov</h1>
            <p className="text-gray-700 max-w-md text-center md:text-left">
              I've put together this list of my favorite AI tools that I use every day. These tools make everything
              fast. Explore, try them out, and see how they can help with your projects!
            </p>
          </div>
        </div>
      </div>

      {/* Right column - fixed 820px on large screens, 380px on small screens */}
      <div className="w-[380px] lg:w-[820px] p-8">


        {/* Text Assistants Section */}
        <div className="mb-12">
          <h2 className="text-xl font-semibold mb-4">Text Assistants</h2>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <ToolCard
              name="ChatGPT"
              description="My favorite AI assistant for generating ideas, drafting content, and brainstorming solutions. A must-have for any creative professional."
              color="bg-emerald-200"
              icon="🧠"
            />
            <ToolCard
              name="Perplexity"
              description="A powerful research assistant, great for knowledge gathering."
              color="bg-slate-800"
              icon="🔍"
              textColor="text-white"
            />
            <ToolCard
              name="Claude"
              description="An assistant for writing, transforming, and expanding ideas."
              color="bg-orange-400"
              icon="☀️"
            />
          </div>
        </div>

        {/* Image Generators Section */}
        <div className="mb-12">
          <h2 className="text-xl font-semibold mb-4">Image Generators</h2>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <ToolCard
              name="VisualElectric"
              description="Turn your ideas into cool visuals with this AI image generator!"
              color="bg-red-500"
              icon="⚡"
              textColor="text-white"
            />
            <ToolCard
              name="Ideogram"
              description="Stunning visuals with the ability to add custom text."
              color="bg-black"
              icon="🔠"
              textColor="text-white"
            />
            <ToolCard
              name="MidJourney"
              description="The gold standard for generating hyper-realistic visuals and conceptual art. This is my go-to AI tool for creative projects, and I use it most of the time."
              color="bg-gray-100"
              icon="🔱"
            />
            <ToolCard
              name="Krea AI"
              description="Love their Edit Tool, that is perfect for seamless adjustments."
              color="bg-white border border-gray-200"
              icon="🎨"
            />
            <ToolCard
              name="Recraft"
              description="Create your own custom style for generating consistent and stunning visuals. Perfect for brand designs that match your vision! Sign up using the link and earn 200 credits."
              color="bg-purple-500"
              icon="🔄"
              textColor="text-white"
              isWide={true}
            />
            <ToolCard
              name="Adobe Firefly"
              description="Great for different tasks, perfect for designers."
              color="bg-red-600"
              icon="🔥"
              textColor="text-white"
            />
          </div>
        </div>

        {/* Content Creation Section */}
        <div className="mb-12">
          <h2 className="text-xl font-semibold mb-4">Content Creation</h2>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <ToolCard
              name="Chatplace"
              description="I use Chatplace to create content faster."
              color="bg-purple-200"
              icon="💬"
              isWide={false}
            />
            {/* Add more content creation tools as needed */}
          </div>
        </div>
      </div>
    </div>
  )
}

interface ToolCardProps {
  name: string
  description: string
  color: string
  icon: string
  textColor?: string
  isWide?: boolean
}

function ToolCard({ name, description, color, icon, textColor = "text-black", isWide = false }: ToolCardProps) {
  return (
    <div className={`${color} ${isWide ? "md:col-span-2" : ""} rounded-xl p-6 relative overflow-hidden group`}>
      <div className="absolute top-3 right-3 bg-white/20 rounded-full p-1 opacity-70">
        <ExternalLink size={16} className={textColor} />
      </div>
      <div className="flex flex-col h-full">
        <div className="text-2xl mb-2">{icon}</div>
        <h3 className={`text-xl font-semibold mb-2 ${textColor}`}>{name}</h3>
        <p className={`${textColor} text-sm opacity-90`}>{description}</p>
      </div>
    </div>
  )
}

