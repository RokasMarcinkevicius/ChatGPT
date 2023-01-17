// Main Open AI response object
public class OpenAIResponse
{
    // Auto-generated
    public string? id { get; set; }

    // Target job
    public string? @object { get; set; }
    public int created { get; set; }
    
    // Engine used
    public string? model { get; set; }
    
    // API response
    public List<Choice>? choices { get; set; }
    
    // Tokens used
    public Usage? usage { get; set; }
}

// Text response
public class Choice
{
    // Text response
    public string? text { get; set; }
    public int index { get; set; }
    public object? logprobs { get; set; }
    // Why generation ended
    public string? finish_reason { get; set; }
}

// API Token usage
public class Usage
{
    // Difficulty of input
    public int prompt_tokens { get; set; }
    // Difficulty of output
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}

